import { ref, type Ref } from 'vue'
import { supabase } from '@/lib/supabase'

export interface DatabaseOperations {
  loading: Ref<boolean>
  error: Ref<string | null>
  fetchData: <T>(table: string, query?: any) => Promise<T[] | null>
  insertData: <T>(table: string, data: any) => Promise<T | null>
  updateData: <T>(table: string, id: string, data: any) => Promise<T | null>
  deleteData: (table: string, id: string) => Promise<boolean>
}

export function useSupabase(): DatabaseOperations {
  const loading = ref(false)
  const error = ref<string | null>(null)

  const fetchData = async <T>(table: string, query?: any): Promise<T[] | null> => {
    loading.value = true
    error.value = null
    
    try {
      let supabaseQuery = supabase.from(table).select('*')
      
      if (query) {
        Object.entries(query).forEach(([key, value]) => {
          supabaseQuery = supabaseQuery.eq(key, value)
        })
      }
      
      const { data, error: fetchError } = await supabaseQuery
      
      if (fetchError) {
        error.value = fetchError.message
        return null
      }
      
      return data as T[]
    } catch (err) {
      error.value = err instanceof Error ? err.message : 'An error occurred'
      return null
    } finally {
      loading.value = false
    }
  }

  const insertData = async <T>(table: string, data: any): Promise<T | null> => {
    loading.value = true
    error.value = null
    
    try {
      const { data: insertedData, error: insertError } = await supabase
        .from(table)
        .insert(data)
        .select()
        .single()
      
      if (insertError) {
        error.value = insertError.message
        return null
      }
      
      return insertedData as T
    } catch (err) {
      error.value = err instanceof Error ? err.message : 'An error occurred'
      return null
    } finally {
      loading.value = false
    }
  }

  const updateData = async <T>(table: string, id: string, data: any): Promise<T | null> => {
    loading.value = true
    error.value = null
    
    try {
      const { data: updatedData, error: updateError } = await supabase
        .from(table)
        .update(data)
        .eq('id', id)
        .select()
        .single()
      
      if (updateError) {
        error.value = updateError.message
        return null
      }
      
      return updatedData as T
    } catch (err) {
      error.value = err instanceof Error ? err.message : 'An error occurred'
      return null
    } finally {
      loading.value = false
    }
  }

  const deleteData = async (table: string, id: string): Promise<boolean> => {
    loading.value = true
    error.value = null
    
    try {
      const { error: deleteError } = await supabase
        .from(table)
        .delete()
        .eq('id', id)
      
      if (deleteError) {
        error.value = deleteError.message
        return false
      }
      
      return true
    } catch (err) {
      error.value = err instanceof Error ? err.message : 'An error occurred'
      return false
    } finally {
      loading.value = false
    }
  }

  return {
    loading,
    error,
    fetchData,
    insertData,
    updateData,
    deleteData
  }
}